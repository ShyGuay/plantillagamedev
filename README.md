# plantillagamedev

Plantilla para crear juegos 2D en C# sin depender de Unity, Godot o Unreal. El objetivo es convertir documentacion + prompt en un vertical slice jugable con herramientas visuales para iterar rapido.

## Que incluye

- `Game.Core`: logica portable, modelo de escena, simulacion y serializacion JSON.
- `Game.Editor`: estado de editor, seleccion, duplicado, Play/Stop y helpers de inspector.
- `Game.Web`: editor visual en Blazor WebAssembly con viewport SVG, jerarquia, inspector y Play mode.
- `content/`: escena y tuning base en JSON.
- `docs/design/`: brief y backlog inicial para vertical slices.
- `tests/Game.Core.Tests`: pruebas ejecutables sin frameworks externos.

## Requisitos

- VS Code.
- .NET SDK 8 o superior compatible con `net8.0`.
- Extension recomendada: C# Dev Kit.

## Arranque rapido

```bash
dotnet restore GameDevTemplate.sln
dotnet run --project src/Game.Web/Game.Web.csproj
```

Abre la URL local que imprima `dotnet run`. La primera pantalla es el editor.

## Flujo de iteracion

1. Edita entidades desde la jerarquia y el inspector.
2. Cambia posicion, escala, color, sprite y valores de gameplay.
3. Pulsa `Play` para ejecutar el juego con el estado actual del editor.
4. Mueve al jugador con `WASD` o flechas.
5. Llega al objetivo para completar la slice.
6. Usa `Export JSON` para copiar la escena y guardarla en `content/scenes/main.scene.json`.

## Comandos utiles

```bash
dotnet build GameDevTemplate.sln
dotnet run --project tests/Game.Core.Tests/Game.Core.Tests.csproj
dotnet publish src/Game.Web/Game.Web.csproj -c Release -o publish/web
```

## Como usar esta plantilla con Codex

Pasa una documentacion y un prompt de features. El trabajo esperado es:

1. Actualizar `docs/design/game_brief.md`.
2. Actualizar `docs/design/vertical_slice_backlog.md`.
3. Crear o modificar contenido JSON.
4. Implementar sistemas de gameplay en `Game.Core`.
5. Exponer valores editables en `Game.Editor` y `Game.Web`.
6. Verificar que la slice es jugable de principio a fin.

La regla practica: si una variable se va a ajustar durante diseno, debe vivir en JSON o en el editor, no como constante enterrada en codigo.
