# Vertical Slice Backlog

## Playable Contract

- [x] The game launches into a clear editor-first flow.
- [x] The player understands the objective within one minute.
- [x] The core loop is playable from start to finish.
- [x] The slice has a win condition.

## Editor Contract

- [x] The main scene can be inspected visually.
- [x] Key gameplay values can be changed without recompiling.
- [x] Play mode uses current editor state.
- [x] Content can be exported as JSON.

## Build Contract

- [x] Development run command documented.
- [ ] Native Windows host deferred until the first real game needs it.
- [x] Web path defined through Blazor WebAssembly.

## Acceptance Tests

- [ ] First launch works from a clean checkout.
- [x] Restart returns to a playable state.
- [x] Exporting editor changes produces scene JSON.
- [x] Loading scene JSON recreates the edited scene.
- [x] No blocking errors in the core runtime tests.

## Cut List

- Native Windows renderer host.
- Audio mixer.
- Tilemap paint tools.
- Asset import pipeline.
- Gamepad input.
