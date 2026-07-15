# Game Design Brief

## Source Prompt

Plantilla base para crear juegos 2D en C# desde documentacion y prompts de features.

## Player Fantasy

El jugador controla una entidad clara, aprende el objetivo en segundos y puede completar una micro-slice jugable.

## Core Loop

1. Leer objetivo.
2. Moverse por la escena.
3. Alcanzar/interactuar con el objetivo.
4. Reiniciar o modificar valores en el editor y volver a probar.

## Controls

- Move: WASD o flechas.
- Primary action: reservado para cada juego.
- Secondary action: reservado para cada juego.
- Pause/restart: boton Reset del editor.

## Slice Objective

Llegar al objetivo verde con el jugador azul.

## Fail State

La plantilla no incluye fail state por defecto. Cada juego debe definirlo en su vertical slice.

## Visual Direction

Formas planas y colores legibles como placeholders. Los assets finales se conectan desde JSON y el inspector.

## Systems

- Player: movimiento 2D con velocidad editable.
- World: limites de escena.
- Enemies/hazards: diferidos.
- Interactions: colision de proximidad con objetivo.
- UI: estado de editor, mensaje de victoria y controles Play/Stop.
- Audio: diferido.

## Editor Needs

- Jerarquia de entidades.
- Viewport visual.
- Inspector de transform, sprite/color y componentes.
- Play mode que usa el estado actual del editor.
- Export JSON para persistir cambios.
