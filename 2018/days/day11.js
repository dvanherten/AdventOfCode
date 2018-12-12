"use strict";

// Part 1
// ======

const part1 = () => {
  const serialNumber = 8141;
  const gridSummed = buildGrid(serialNumber);
  let result = {};
  identifyLargestBlock(gridSummed, 3, result);
  return `${result.x},${result.y}`;
};

// Part 2
// ======

/* 
  Had Anthony's help to understand the algorithm.
  
  Essentially every coordinate is summed based on the X Y before it when building the grid
    [1, 2, 3] => [1, 3,  6 ]
    [2, 3, 4] => [3, 8,  15]
    [3, 4, 5] => [6, 15, 27]
  To calculate the bottom right 2,2 block it is:
  (bottom right corner) - (reduce X by size) - (reduce y by size) + (reduce both by size)
  (3,3) - (1,3) - (3,1) + (1,1)
  27 - 6 - 6 + 1 = 16 

  Really slick. Apparently there's an even better algorithm, but this one works pretty well.
*/

const part2 = () => {
  const serialNumber = 8141;
  const gridSummed = buildGrid(serialNumber);
  let result = {};
  for (let size = 1; size <= 300; size++) {
    identifyLargestBlock(gridSummed, size, result);
  }
  return `${result.x},${result.y},${result.size}`;
};

module.exports = { part1, part2 };

const calculateFuelLevel = (x, y, serialNumber) => {
  const rackId = x + 10;
  const preSlice = (rackId * y + serialNumber) * rackId;
  return +(preSlice.toString().slice(-3)[0] || 0) - 5;
};

const buildGrid = serialNumber => {
  const gridSummed = {};
  for (let x = 1; x <= 300; x++) {
    for (let y = 1; y <= 300; y++) {
      const powerlevel = calculateFuelLevel(x, y, serialNumber);
      gridSummed[`${x}-${y}`] =
        powerlevel +
        (gridSummed[`${x}-${y - 1}`] || 0) +
        (gridSummed[`${x - 1}-${y}`] || 0) -
        (gridSummed[`${x - 1}-${y - 1}`] || 0);
    }
  }
  return gridSummed;
};

const identifyLargestBlock = (gridSummed, size, currentMax) => {
  for (let x = size; x <= 300; x++) {
    for (let y = size; y <= 300; y++) {
      const localTotal =
        gridSummed[`${x}-${y}`] -
        gridSummed[`${x - size}-${y}`] -
        gridSummed[`${x}-${y - size}`] +
        gridSummed[`${x - size}-${y - size}`];
      if (localTotal > (currentMax.max || 0)) {
        currentMax.max = localTotal;
        currentMax.x = x - size + 1;
        currentMax.y = y - size + 1;
        currentMax.size = size;
      }
    }
  }
};
