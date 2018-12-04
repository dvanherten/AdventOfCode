"use strict";

// Part 1
// ======

const part1 = input => {
  const claimIds = input.split("\n");
  // Setup map
  const map = new Array(1000);
  for (let i = 0; i < map.length; i++) map[i] = new Array(1000);

  // Map and Count
  let doubleCounted = 0;
  claimIds
    .map(x => parseClaimId(x))
    .forEach(claimId => {
      for (let w = 1; w <= claimId.width; w++) {
        for (let h = 1; h <= claimId.height; h++) {
          const x = claimId.left + w;
          const y = claimId.top + h;
          if (map[x][y] === undefined) map[x][y] = false;
          else if (map[x][y] === false) {
            map[x][y] = true;
            doubleCounted++;
          }
        }
      }
    });
  return doubleCounted;
};

// Part 2
// ======

const part2 = input => {
  const claimIds = input.split("\n");
  // Setup map
  const map = new Array(1000);
  for (let i = 0; i < map.length; i++) map[i] = new Array(1000);

  const parsedIds = claimIds.map(x => parseClaimId(x));

  // Map it
  parsedIds.forEach(claimId => {
    for (let w = 1; w <= claimId.width; w++) {
      for (let h = 1; h <= claimId.height; h++) {
        const x = claimId.left + w;
        const y = claimId.top + h;
        if (map[x][y] === undefined) map[x][y] = false;
        else if (map[x][y] === false) {
          map[x][y] = true;
        }
      }
    }
  });

  // Find good claim.
  for (let i = 0; i < parsedIds.length; i++) {
    const claimId = parsedIds[i];
    let badClaim = false;
    for (let w = 1; w <= claimId.width; w++) {
      for (let h = 1; h <= claimId.height; h++) {
        const x = claimId.left + w;
        const y = claimId.top + h;
        if (map[x][y] === true) {
          badClaim = true;
          break;
        }
      }
      if (badClaim) break;
    }
    if (!badClaim) return claimId.id;
  }

  return "RIP";
};

module.exports = { part1, part2 };

const parseClaimId = input => {
  const regEx = /#([\d]+) @ ([\d]+),([\d]+): ([\d]+)x([\d]+)/;
  const result = regEx.exec(input);
  return {
    id: result[1],
    left: +result[2],
    top: +result[3],
    width: +result[4],
    height: +result[5]
  };
};
