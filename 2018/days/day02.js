"use strict";

// Part 1
// ======

const part1 = input => {
  const boxIds = input.split("\n");
  let twoCounter = 0;
  let threeCounter = 0;
  for (let i = 0; i < boxIds.length; i++) {
    const characterGroups = getCharacterGroups(boxIds[i]);

    let twoCounted = false;
    let threeCounted = false;

    for (let character in characterGroups) {
      if (characterGroups[character] === 2) {
        if (!twoCounted) twoCounter++;
        twoCounted = true;
      } else if (characterGroups[character] === 3) {
        if (!threeCounted) threeCounter++;
        threeCounted = true;
      }
    }
  }
  return twoCounter * threeCounter;
};

const getCharacterGroups = boxId => {
  const groups = {};
  for (let i = 0; i < boxId.length; i++) {
    if (groups[`${boxId[i]}`]) groups[`${boxId[i]}`]++;
    else groups[`${boxId[i]}`] = 1;
  }
  return groups;
};

// Part 2
// ======

const part2 = input => {
  const boxIds = input.split("\n");
  const seen = new Set();
  for (let i = 0; i < boxIds[0].length; i++) {
    for (let j = 0; j < boxIds.length; j++) {
      const withoutIndexBoxId = withoutIndex(boxIds[j], i);
      if (seen.has(i + withoutIndexBoxId)) return withoutIndexBoxId;
      seen.add(i + withoutIndexBoxId);
    }
  }
};

const withoutIndex = (boxId, i) => {
  return boxId.substr(0, i) + boxId.substr(i + 1, boxId.length);
};

module.exports = { part1, part2 };
