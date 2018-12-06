"use strict";

// Part 1
// ======

const part1 = input => {
  return input.split("").reduce(alchemicalReducer).length;
};

// Part 2
// ======

const part2 = input => {
  const lengths = [];
  for (
    let uppercaseCode = "A".charCodeAt(0);
    uppercaseCode <= "Z".charCodeAt(0);
    uppercaseCode++
  ) {
    lengths.push(
      input
        .replace(
          new RegExp(
            `[${String.fromCharCode(uppercaseCode, uppercaseCode + 32)}]`,
            "g"
          ),
          ""
        )
        .split("")
        .reduce(alchemicalReducer).length
    );
  }
  return Math.min(...lengths);
};

module.exports = { part1, part2 };

const alchemicalReducer = (reduced, unitToProcess) => {
  let working = reduced || "";
  working += unitToProcess;
  if (reacts(working.slice(-2))) return working.slice(0, -2);
  return working;
};

const reacts = twoChars => {
  if (twoChars.length != 2) return false;
  return Math.abs(twoChars.charCodeAt(0) - twoChars.charCodeAt(1)) === 32;
};
