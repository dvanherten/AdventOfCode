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
  const frequenciesSeen = [];

  let valuesFoundTwice = null;
  const trackFrequency = (accumulator, currentValue) => {
    const value = accumulator + currentValue;
    if (frequenciesSeen.includes(value) && valuesFoundTwice == null)
      valuesFoundTwice = value;
    else frequenciesSeen.push(value);
    return value;
  };

  let currentFrequency = 0;
  let endlessPrevention = 0;
  while (valuesFoundTwice == null || endlessPrevention === 100) {
    currentFrequency = frequencyChanges.reduce(
      trackFrequency,
      currentFrequency
    );
    endlessPrevention++;
  }
  return valuesFoundTwice;
};

module.exports = { part1, part2 };
