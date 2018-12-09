"use strict";

const testInput = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

// Part 1
// ======

const part1 = input => {
  const result = input.split(" ").reduce(parseData, INITIAL_STATE);
  return result.totalMetadata;
};

const parseData = (treeInfo, data) => {
  return dataReducer(treeInfo, {
    action: treeInfo.nextAction,
    data: +data
  });
};

// Part 2
// ======

const part2 = input => {
  const result = input.split(" ").reduce(parseData, INITIAL_STATE);
  return result.nodes.calcValue();
};

module.exports = { part1, part2 };

const IDENTIFY_CHILD_NODES = "IDENTIFY_CHILD_NODES";
const IDENTIFY_METADATA_AMOUNT = "IDENTIFY_METADATA_AMOUNT";
const PROCESS_METADATA = "PROCESS_METADATA";

const INITIAL_STATE = {
  nextAction: IDENTIFY_CHILD_NODES,
  totalMetadata: 0,
  workingStack: [],
  nodes: null
};

const dataReducer = (state, { action, data }) => {
  // console.log(state);
  // console.log(action);
  // console.log(data);
  switch (action) {
    case IDENTIFY_CHILD_NODES: {
      // Add a new node with the child data.
      const newNode = {
        children: [],
        metadata: [],
        childrenCount: data,
        metadataCount: null,
        calcValue: function() {
          return this.children.length === 0
            ? this.metadata.reduce((a, b) => a + b, 0)
            : this.metadata.reduce((total, m) => {
                if (m != 0 && m <= this.children.length)
                  return total + this.children[m - 1].calcValue();
                return total;
              }, 0);
        }
      };

      // if we are working on a new child node, decrement the previous one.
      const lastNode = state.workingStack.slice(-1).pop();
      if (lastNode != undefined) {
        lastNode.childrenCount = lastNode.childrenCount - 1;
        lastNode.children.push(newNode);
      }
      const nodes = state.nodes == null ? newNode : state.nodes;
      const workingStack = [...state.workingStack, newNode];

      // Setup for the metadata step next.
      return {
        ...state,
        nodes: nodes,
        workingStack: workingStack,
        nextAction: IDENTIFY_METADATA_AMOUNT
      };
    }

    case IDENTIFY_METADATA_AMOUNT: {
      // apply metadata to current node.
      const lastNode = state.workingStack.slice(-1).pop();
      lastNode.metadataCount = data;

      // if node has no more child nodes, start processing metadata.
      const nextAction =
        lastNode.childrenCount > 0 ? IDENTIFY_CHILD_NODES : PROCESS_METADATA;

      return {
        ...state,
        workingStack: [...state.workingStack.slice(0, -1), lastNode],
        nextAction: nextAction
      };
    }

    case PROCESS_METADATA: {
      const newTotal = state.totalMetadata + data;

      const lastNode = state.workingStack.slice(-1).pop();
      lastNode.metadataCount--;
      lastNode.metadata.push(data);

      // when done processing metadata, determine whether we need
      // to process another child or continue metadata on the parent node.
      if (lastNode.metadataCount === 0) {
        const workingStack = [...state.workingStack.slice(0, -1)];
        const newLastNode = workingStack.slice(-1).pop();

        if (newLastNode) {
          const nextAction =
            newLastNode.childrenCount > 0
              ? IDENTIFY_CHILD_NODES
              : PROCESS_METADATA;

          return {
            ...state,
            nodeCount: state.nodeCount - 1,
            workingStack: workingStack,
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
        workingStack: [...state.workingStack.slice(0, -1), lastNode],
        nextAction: PROCESS_METADATA,
        totalMetadata: newTotal
      };
    }

    default:
      return state;
  }
};
