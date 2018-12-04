"use strict";

// Part 1
// ======

const part1 = input => {
  const timeRecords = input
    .split("\n")
    .map(x => parseTimeRecord(x))
    .sort((a, b) => a.date - b.date);

  let currentGuard = "";
  const instructionParser = (guardTracker, record, index, sourceArray) => {
    if (record.instruction.startsWith("G"))
      currentGuard = getGuardNumber(record.instruction);
    if (record.instruction.startsWith("w")) {
      const sleepRecord = sourceArray[index - 1];
      const sleepMin = sleepRecord.hours === 23 ? 0 : sleepRecord.minutes;
      const wakeMin = record.hours === 1 ? 60 : record.minutes;

      if (guardTracker[currentGuard] === undefined)
        guardTracker[currentGuard] = new Array(60);

      for (let i = sleepMin; i < wakeMin; i++) {
        if (guardTracker[currentGuard][i] === undefined)
          guardTracker[currentGuard][i] = 1;
        else guardTracker[currentGuard][i]++;
      }
    }

    return guardTracker;
  };

  const guards = timeRecords.reduce(instructionParser, {});

  let maxSleepTime = 0;
  let guardNum;
  Object.keys(guards).forEach(key => {
    const sleepTime = guards[key].reduce(sum);
    if (sleepTime > maxSleepTime) {
      maxSleepTime = sleepTime;
      guardNum = key;
    }
  });

  const max = (prev, current, index) => {
    if (prev > current) return prev;
    maxMinute = index;
    return current;
  };

  let maxMinute = 0;
  guards[guardNum].reduce(max);

  return maxMinute * +guardNum;
};

// Part 2
// ======

const part2 = input => {
  const timeRecords = input
    .split("\n")
    .map(x => parseTimeRecord(x))
    .sort((a, b) => a.date - b.date);

  let currentGuard = "";
  const instructionParser = (guardTracker, record, index, sourceArray) => {
    if (record.instruction.startsWith("G"))
      currentGuard = getGuardNumber(record.instruction);
    if (record.instruction.startsWith("w")) {
      const sleepRecord = sourceArray[index - 1];
      const sleepMin = sleepRecord.hours === 23 ? 0 : sleepRecord.minutes;
      const wakeMin = record.hours === 1 ? 60 : record.minutes;

      if (guardTracker[currentGuard] === undefined)
        guardTracker[currentGuard] = new Array(60);

      for (let i = sleepMin; i < wakeMin; i++) {
        if (guardTracker[currentGuard][i] === undefined)
          guardTracker[currentGuard][i] = 1;
        else guardTracker[currentGuard][i]++;
      }
    }

    return guardTracker;
  };

  const guards = timeRecords.reduce(instructionParser, {});

  let currentMinute = 0;
  let currentMax = 0;
  let maxMinute = 0;
  let guardNum;

  const max = (prev, current, index) => {
    if (prev > current) return prev;
    currentMinute = index;
    return current;
  };

  Object.keys(guards).forEach(key => {
    const maxSleepTime = guards[key].reduce(max);
    if (maxSleepTime > currentMax) {
      maxMinute = currentMinute;
      currentMax = maxSleepTime;
      guardNum = key;
    }
  });

  return maxMinute * +guardNum;
};

module.exports = { part1, part2 };

const parseTimeRecord = input => {
  const regEx = /\[(.* (.+):(.+))\] (.+)/;
  const result = regEx.exec(input);
  return {
    date: new Date(result[1]),
    hours: +result[2],
    minutes: +result[3],
    instruction: result[4]
  };
};

const getGuardNumber = instruction => {
  const regEx = /Guard #([\d]+) begins shift/;
  const result = regEx.exec(instruction);
  return result[1];
};

const sum = (total, num) => {
  return total + num;
};
