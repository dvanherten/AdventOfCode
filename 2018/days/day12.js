"use strict";

const testInput = `initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #`;

// Part 1
// ======

const part1 = input => {
  const parsedInput = parse(input);
  const rules = parsedInput.rules.map(r => {
    const [ruleString, outputString] = r.split(" => ");
    const rule = ruleString.split("").map(toBool);
    return {
      rule: rule,
      output: toBool(outputString)
    };
  });
  const negativeSpace = 10;
  const gen0 = [...new Array(negativeSpace).fill(false)]
    .concat(parsedInput.initial.split("").map(toBool))
    .concat([...new Array(20).fill(false)]);
  const generations = [];
  generations.push(gen0);
  const length = gen0.length;
  let prev = gen0;
  for (let c = 1; c <= 20; c++) {
    const next = new Array(length).fill(false);
    for (let r = 0; r < rules.length; r++) {
      for (let section = 5; section < length; section++) {
        const slice = prev.slice(section - 5, section);
        if (slice.every((x, i) => x === rules[r].rule[i]))
          next[section - 3] = rules[r].output;
      }
    }
    generations.push(next);
    prev = next;
  }
  generations.forEach(gen =>
    console.log(gen.map(x => (x ? "#" : ".")).join(""))
  );
  return generations
    .slice(-1)[0]
    .reduce(
      (total, val, index) => total + (val ? index - negativeSpace : 0),
      0
    );
};

// Part 2
// ======

const part2 = input => {
  const parsedInput = parse(input);
  const rules = parsedInput.rules.map(r => {
    const [ruleString, outputString] = r.split(" => ");
    const rule = ruleString.split("").map(toBool);
    return {
      rule: rule,
      output: toBool(outputString)
    };
  });

  const negativeSpace = 5;
  const gen0 = [...new Array(negativeSpace).fill(false)]
    .concat(parsedInput.initial.split("").map(toBool))
    .concat([...new Array(1000).fill(false)]); // Allow for lots of growth on the positive side.

  const generations = [];
  generations.push(gen0);
  const length = gen0.length;

  let prev = gen0;
  let lastSum = 0;
  let lastDiff = 0;
  let sameDiffCounter = 0;

  let counter = 0;
  // Look for linear progression
  for (counter; counter < 1000; counter++) {
    const next = new Array(length).fill(false);
    for (let r = 0; r < rules.length; r++) {
      for (let section = 5; section < length; section++) {
        const slice = prev.slice(section - 5, section);
        if (slice.every((x, i) => x === rules[r].rule[i]))
          next[section - 3] = rules[r].output;
      }
    }

    const newSum = next.reduce(
      (total, val, index) => total + (val ? index - negativeSpace : 0),
      0
    );
    const newDiff = newSum - lastSum;
    if (newDiff == lastDiff) sameDiffCounter++;
    else sameDiffCounter = 0;
    if (sameDiffCounter == 5) break; // Assumption that if we see the same diff 5 times in a row we will repeat forever.

    lastSum = newSum;
    lastDiff = newDiff;
    generations.push(next);
    prev = next;
  }
  const wayTooManyIterations = 50000000000;
  return lastSum + (wayTooManyIterations - counter) * lastDiff;
};

module.exports = { part1, part2 };

const parse = input => {
  const split = input.split("\n");
  const [first, _, ...rest] = split;
  return {
    initial: first.slice(15),
    rules: rest
  };
};

const toBool = c => c === "#";
