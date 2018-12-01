"use strict";

// Part 1
// ======

const part1 = input => {
  const frequencyChanges = input.split("\n").map(x => +x);
  const trackFrequency = (frequency, change) => frequency + change;
  const result = frequencyChanges.reduce(trackFrequency);
  return result;
};

// Part 2
// ======

const part2 = input => {
  const frequencyChanges = input.split("\n").map(x => +x);
  const frequenciesSeen = new Set();
  let frequency = 0;
  while (true) {
    for (let i = 0; i < frequencyChanges.length; i++) {
      frequency += frequencyChanges[i];
      if (frequenciesSeen.has(frequency)) return frequency;
      frequenciesSeen.add(frequency);
    }
  }
};

module.exports = { part1, part2 };
