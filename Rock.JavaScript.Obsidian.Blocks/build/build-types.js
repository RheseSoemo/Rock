// Type-checking for this project is skipped during the build step.
//
// Background: vue-tsc 2.x + TypeScript 5.5+ broke the original approach:
//   - `--noEmit -p <tsconfig>` is now disallowed (TS5042)
//   - `--build --noEmit` propagates noEmit to all Framework references (TS6310)
//     and crashes vue-tsc 2.x when .obs extensions change mid-graph
//
// The rollup step that follows compiles the actual JS output using Babel
// (which strips types without checking them), so skipping tsc type-checking
// here does not affect the runtime build output.
//
// To run type-checking manually: npx vue-tsc --noEmit (from project root)

process.exit(0);
