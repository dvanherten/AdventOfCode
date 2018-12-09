"use strict";

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
  return input;
};

module.exports = { part1, part2 };

const IDENTIFY_CHILD_NODES = "IDENTIFY_CHILD_NODES";
const IDENTIFY_METADATA_AMOUNT = "IDENTIFY_METADATA_AMOUNT";
const PROCESS_METADATA = "PROCESS_METADATA";

const INITIAL_STATE = {
  nodeCount: 0,
  nextAction: IDENTIFY_CHILD_NODES,
  totalMetadata: 0,
  nodes: []
};

const dataReducer = (state, { action, data }) => {
  // console.log(state);
  // console.log(action);
  // console.log(data);
  switch (action) {
    case IDENTIFY_CHILD_NODES: {
      const lastNode = state.nodes.slice(-1).pop();
      if (lastNode != undefined)
        lastNode.childrenCount = lastNode.childrenCount - 1;
      const nodes = [
        ...state.nodes,
        {
          childrenCount: data,
          metadataCount: null
        }
      ];
      return {
        ...state,
        nodeCount: state.nodeCount + 1,
        nodes: nodes,
        nextAction: IDENTIFY_METADATA_AMOUNT
      };
    }
    case IDENTIFY_METADATA_AMOUNT: {
      const lastNode = state.nodes.slice(-1).pop();
      lastNode.metadataCount = data;
      const nextAction =
        lastNode.childrenCount > 0 ? IDENTIFY_CHILD_NODES : PROCESS_METADATA;

      return {
        ...state,
        nodes: [...state.nodes.slice(0, -1), lastNode],
        nextAction: nextAction
      };
    }
    case PROCESS_METADATA: {
      const lastNode = state.nodes.slice(-1).pop();
      const newTotal = state.totalMetadata + data;
      lastNode.metadataCount--;

      if (lastNode.metadataCount === 0) {
        const nodes = [...state.nodes.slice(0, -1)];
        const newLastNode = nodes.slice(-1).pop();
        if (newLastNode) {
          const nextAction =
            newLastNode.childrenCount > 0
              ? IDENTIFY_CHILD_NODES
              : PROCESS_METADATA;

          return {
            ...state,
            nodeCount: state.nodeCount - 1,
            nodes: nodes,
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
        nodes: [...state.nodes.slice(0, -1), lastNode],
        nextAction: PROCESS_METADATA,
        totalMetadata: newTotal
      };
    }

    default:
      return state;
  }
};
