"use strict";

const testInput = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

// Part 1
// ======

const part1 = input => {
  const result = input.split(" ").reduce(parseData, INITIAL_STATE);
  return result.totalMetadata;
};

const parseData = (treeInfo, data, index) => {
  return dataReducer(treeInfo, {
    action: treeInfo.nextAction,
    data: +data
  });
};

// Part 2
// ======

const part2 = input => {
  const result = testInput.split(" ").reduce(parseData, INITIAL_STATE);
  return result.totalMetadata;
};

module.exports = { part1, part2 };

const IDENTIFY_CHILD_NODES = "IDENTIFY_CHILD_NODES";
const IDENTIFY_METADATA_AMOUNT = "IDENTIFY_METADATA_AMOUNT";
const PROCESS_METADATA = "PROCESS_METADATA";

const INITIAL_STATE = {
  nextAction: IDENTIFY_CHILD_NODES,
  totalMetadata: 0,
  nodeStack: []
};

const dataReducer = (state, { action, data }) => {
  // console.log(state);
  // console.log(action);
  // console.log(data);
  switch (action) {
    case IDENTIFY_CHILD_NODES: {
      // if we are working on a new child node, decrement the previous one.
      const lastNode = state.nodeStack.slice(-1).pop();
      if (lastNode != undefined)
        lastNode.childrenCount = lastNode.childrenCount - 1;
      // Add a new node with the child data.
      const nodeStack = [
        ...state.nodeStack,
        {
          childrenCount: data,
          metadataCount: null
        }
      ];

      // Setup for the metadata step next.
      return {
        ...state,
        nodeStack: nodeStack,
        nextAction: IDENTIFY_METADATA_AMOUNT
      };
    }

    case IDENTIFY_METADATA_AMOUNT: {
      // apply metadata to current node.
      const lastNode = state.nodeStack.slice(-1).pop();
      lastNode.metadataCount = data;

      // if node has no more child nodeStack, start processing metadata.
      const nextAction =
        lastNode.childrenCount > 0 ? IDENTIFY_CHILD_NODES : PROCESS_METADATA;

      return {
        ...state,
        nodeStack: [...state.nodeStack.slice(0, -1), lastNode],
        nextAction: nextAction
      };
    }

    case PROCESS_METADATA: {
      const newTotal = state.totalMetadata + data;

      const lastNode = state.nodeStack.slice(-1).pop();
      lastNode.metadataCount--;

      // when done processing metadata, determine whether we need
      // to process another child or continue metadata on the parent node.
      if (lastNode.metadataCount === 0) {
        const nodeStack = [...state.nodeStack.slice(0, -1)];
        const newLastNode = nodeStack.slice(-1).pop();
        if (newLastNode) {
          const nextAction =
            newLastNode.childrenCount > 0
              ? IDENTIFY_CHILD_NODES
              : PROCESS_METADATA;

          return {
            ...state,
            nodeCount: state.nodeCount - 1,
            nodeStack: nodeStack,
            nextAction: nextAction,
            totalMetadata: newTotal
          };
        } else
          return {
            ...state,
            nextAction: "ALL_DONE",
            totalMetadata: newTotal
          };
      }

      return {
        ...state,
        nodeStack: [...state.nodeStack.slice(0, -1), lastNode],
        nextAction: PROCESS_METADATA,
        totalMetadata: newTotal
      };
    }

    default:
      return state;
  }
};
