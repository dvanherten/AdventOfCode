"use strict";

// Part 1
// ======

// My straight forward normal JS Array answer.
const part1 = input => {
  const [playerCount, marbleCount] = input.match(/\d+/g).map(Number);
  const players = new Array(playerCount).fill(0);

  let currentPlayer = 1;
  let currentMarbleIndex = 0;
  let marbleWorth = 0;
  const marbles = [0];
  for (let marbleCounter = 1; marbleCounter <= marbleCount; marbleCounter++) {
    // get points
    if (marbleCounter % 23 === 0) {
      let newMarbleIndex = currentMarbleIndex - 7;
      newMarbleIndex =
        newMarbleIndex < 0 ? newMarbleIndex + marbles.length : newMarbleIndex;
      const wonMarble = marbles.splice(newMarbleIndex, 1);
      marbleWorth = wonMarble[0] + marbleCounter;
      players[currentPlayer - 1] = players[currentPlayer - 1] + marbleWorth;
      currentMarbleIndex = newMarbleIndex;
    } else {
      // place marble
      let newMarbleIndex = currentMarbleIndex + 2;
      newMarbleIndex =
        newMarbleIndex > marbles.length
          ? newMarbleIndex - marbles.length
          : newMarbleIndex;
      marbles.splice(newMarbleIndex, 0, marbleCounter);
      currentMarbleIndex = newMarbleIndex;
    }

    currentPlayer = (currentPlayer % playerCount) + 1;
  }
  return Math.max(...players);
};

// Part 2
// ======

// Some smart person on the interwebs showing me
// how easy it is to do a doubly linkedlist in JS.
// Pretty slick! #learning
const addAfter = (value, marble) => {
  const toAdd = {
    value,
    prev: marble,
    next: marble.next
  };
  marble.next.prev = toAdd;
  marble.next = toAdd;
  return toAdd;
};

const part2 = input => {
  const [playerCount, marbleCount] = input.match(/\d+/g).map(Number);

  const scores = {};
  for (let i = 1; i <= playerCount; i += 1) {
    scores[i] = 0;
  }
  let currentPlayer = 1;

  let current = {
    value: 0
  };
  current.next = current;
  current.prev = current;

  for (let m = 1; m <= marbleCount * 100; m += 1) {
    if (m % 23 === 0) {
      scores[currentPlayer] += m;
      current = current.prev.prev.prev.prev.prev.prev;
      scores[currentPlayer] += current.prev.value;
      current.prev.prev.next = current;
      current.prev = current.prev.prev;
    } else {
      current = addAfter(m, current.next);
    }
    currentPlayer = (currentPlayer % playerCount) + 1;
  }
  console.log(current.prev);
  return Math.max(...Object.values(scores));
};

module.exports = { part1, part2 };
