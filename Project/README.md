# RAD Project

Small F# project containing streaming data structures, hashing utilities, and experiment harnesses used in the RAD research/learning experiments.

Contents
- `Program.fs` — main entry (experiment runner / example usage)
- `Experiments.fs` — experiment definitions
- `Timing.fs` — timing and benchmarking helpers
- `StreamGenerator.fs` — synthetic stream generation utilities
- `DataStructures/` — implementations such as `CountSketch.fs`, `HashTable.fs`
- `Hashing/` — hashing helpers (`FourUniversal.fs`, `MultiplyModPrime.fs`, `MultiplyShift.fs`)
- `test.json` — example input/config

Prerequisites
- .NET SDK 8.0 or later (install from https://dotnet.microsoft.com)

Quick start
1. Open a terminal in the `Project` directory.
2. Build the project:

```
dotnet build
```

3. Run the project:

```
dotnet run
```

Common tasks
- Clean build artifacts: `dotnet clean`

Notes
- This repository is a research/experiment workspace. Files are organized to make it easy to run small benchmarks and algorithmic experiments.
- To add experiments, update `Experiments.fs` and `Program.fs` to include new experiment cases.

Contributing
- Feel free to open issues or raise pull requests with improvements or bug fixes.

License
- Add a license file if you intend to publish this code. Currently unlicensed.

Contact
- Maintainer: see repository owner or add your contact details here.
