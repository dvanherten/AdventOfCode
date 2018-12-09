"use strict";

// Part 1
// ======

const part1 = input => {
  const steps = input.split("\n").map(toSteps);
};

// Part 2
// ======

const part2 = input => {
  return input;
};

module.exports = { part1, part2 };

const toTree = { tree };

const toSteps = str => {
  const matches = /Step ([A-Z]).*([A-Z])/.exec(str);
  return {
    parent: matches[1],
    child: matches[2]
  };
};
