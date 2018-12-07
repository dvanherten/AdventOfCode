"use strict";

// Part 1
// ======

const part1 = input => {
  const points = input.split("\n").map(toPoints);
  const maxX = Math.max(...points.map(point => point.x));
  const maxY = Math.max(...points.map(point => point.y));

  const grid = new Array(maxX + 1);
  for (let i = 0; i < grid.length; i++) grid[i] = new Array(maxY + 1);

  for (let x = 0; x < maxX; x++) {
    for (let y = 0; y < maxY; y++) {
      const index = closestTo(points, { x: x, y: y });
      grid[x][y] = index;
      if (
        index !== null &&
        (x === 0 || y === 0 || x === maxX - 1 || y === maxY - 1)
      ) {
        points[index].infinite = true;
      }
    }
  }

  const infinitePoints = points.filter(p => p.infinite).map(p => p.id);

  for (let x = 0; x < maxX; x++) {
    for (let y = 0; y < maxY; y++) {
      if (infinitePoints.some(point => point === grid[x][y])) grid[x][y] = null;
    }
  }

  const grouped = grid.reduce(flatten).reduce(groupAndCountAreas, {});
  const largestArea = Math.max(
    ...Object.keys(grouped).map(key => grouped[key])
  );
  return largestArea;
};

// Part 2
// ======

const part2 = input => {
  const points = input.split("\n").map(toPoints);
  const maxX = Math.max(...points.map(point => point.x));
  const maxY = Math.max(...points.map(point => point.y));

  const grid = new Array(maxX + 1);
  for (let i = 0; i < grid.length; i++) grid[i] = new Array(maxY + 1);

  for (let x = 0; x < maxX; x++) {
    for (let y = 0; y < maxY; y++) {
      grid[x][y] = points.reduce((sum, point) => {
        return sum + calculateManhattanDistance(point, { x: x, y: y });
      }, 0);
    }
  }

  return grid.reduce(flatten).reduce((sum, val) => {
    return val < 10000 ? sum + 1 : sum;
  }, 0);
};

module.exports = { part1, part2 };

const calculateManhattanDistance = (starting, destination) => {
  return (
    Math.abs(starting.x - destination.x) + Math.abs(starting.y - destination.y)
  );
};

const closestTo = (points, point) => {
  const distances = points.map(p => calculateManhattanDistance(p, point));
  const min = Math.min(...distances);
  const first = distances.indexOf(min);
  const last = distances.lastIndexOf(min);
  return first === last ? first : null;
};

const flatten = (agg, val) => {
  return agg.concat(val);
};

const groupAndCountAreas = (areas, areaIndex) => {
  if (areaIndex == null) return areas;
  if (areas[areaIndex.toString()] === undefined)
    areas[areaIndex.toString()] = 1;
  else areas[areaIndex.toString()] = areas[areaIndex.toString()] + 1;
  return areas;
};

const pointRegex = new RegExp(/([\d]+), ([\d]+)/);

const toPoints = (str, index) => {
  const matches = pointRegex.exec(str);
  return {
    id: index,
    x: +matches[1],
    y: +matches[2],
    infinite: false
  };
};
