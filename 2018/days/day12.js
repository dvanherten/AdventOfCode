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
  const rules = parsedInput.rules
    .map(r => {
      const [ruleString, outputString] = r.split(" => ");
      const rule = ruleString.split("").map(toBool);
      return {
        rule: rule,
        output: toBool(outputString)
      };
    })
    .filter(r => r.output);
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
  return input;
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
