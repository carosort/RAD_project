# RAD Implementeringsprojekt

F# implementering af to algoritmer til analyse af datastrømme: **hashing med chaining** (eksakt) og **Count-Sketch** (approksimativ). Projektet er en del af kurset Randomiserede Algoritmer og Datastrukturer.

## Projektstruktur

```
Project/
├── Program.fs                  ← Hovedprogram — kør eksperimenter her
├── Experiments.fs              ← Opgave 1, 3, 7 og 8
├── StreamGenerator.fs          ← Genererer teststrømme
├── Timing.fs                   ← Hjælpefunktioner til tidsmåling
├── Hashing/
│   ├── MultiplyShift.fs        ← Opgave 1a: multiply-shift hashing
│   ├── MultiplyModPrime.fs     ← Opgave 1b: multiply-mod-prime hashing
│   └── FourUniversal.fs        ← Opgave 4: 4-universel hashfunktion
├── DataStructures/
│   ├── HashTable.fs            ← Opgave 2: hashtabel med chaining
│   └── CountSketch.fs          ← Opgave 6: Count-Sketch
└── test.json                   ← Output fra eksperimenter (JSON format)
```

## Opgaver

| Opgave | Fil | Beskrivelse |
|--------|-----|-------------|
| 1a | `Hashing/MultiplyShift.fs` | Multiply-shift hashing |
| 1b | `Hashing/MultiplyModPrime.fs` | Multiply-mod-prime hashing |
| 2 | `DataStructures/HashTable.fs` | Hashtabel med chaining (get, set, increment) |
| 3 | `Experiments.fs` → `Opgave_3` | Kvadratsummer med hashtabel |
| 4 | `Hashing/FourUniversal.fs` | 4-universel hashfunktion g(x) |
| 5 | `DataStructures/CountSketch.fs` | Hashfunktioner h og s til Count-Sketch |
| 6 | `DataStructures/CountSketch.fs` | Count-Sketch implementering |
| 7+8 | `Experiments.fs` → `Opgave_7_8` | Count-Sketch eksperimenter |

## Krav

- .NET SDK 8.0 eller nyere — download fra https://dotnet.microsoft.com

## Kør programmet

Åbn en terminal i `Project/` mappen og kør:

```bash
dotnet run
```

Output skrives som JSON til terminalen. For at gemme det til en fil:

```bash
dotnet run > test.json
```

## Parametre

Parametrene styres øverst i `Program.fs`:

```fsharp
let n: int = 1 <<< 25   // strømlængde (2^25 ≈ 33 millioner)
let l: int = 7          // antal forskellige nøgler = 2^l
```

Eksperimenterne der køres:
- `Opgave_1 n l ...` — køretidstest af hashfunktioner
- `Opgave_3 n [|5..25|] ...` — kvadratsummer ved stigende l
- `Opgave_7_8 n l [|5; 10|] ...` — Count-Sketch med forskellige m = 2^t
