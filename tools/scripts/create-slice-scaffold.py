#!/usr/bin/env python3
"""Create docs and content placeholders for a new 2D vertical slice."""

from __future__ import annotations

import argparse
import json
from pathlib import Path


def slugify(value: str) -> str:
    slug = "".join(ch.lower() if ch.isalnum() else "-" for ch in value.strip())
    return "-".join(part for part in slug.split("-") if part) or "untitled-game"


def write_if_missing(path: Path, content: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    if not path.exists():
        path.write_text(content, encoding="utf-8")
        print(f"created {path}")
    else:
        print(f"kept existing {path}")


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--title", required=True)
    parser.add_argument("--root", default=".")
    args = parser.parse_args()

    root = Path(args.root).resolve()
    scene_id = slugify(args.title)
    scene = {
        "version": 1,
        "id": f"{scene_id}-main",
        "name": f"{args.title} Main Scene",
        "settings": {
            "backgroundColor": "#15191f",
            "pixelsPerUnit": 32,
            "worldWidth": 960,
            "worldHeight": 540
        },
        "entities": []
    }

    write_if_missing(root / "docs/design/game_brief.md", f"# {args.title} Game Design Brief\n\n## Source Prompt\n\nDescribe the requested game and feature prompt here.\n")
    write_if_missing(root / "docs/design/vertical_slice_backlog.md", "# Vertical Slice Backlog\n\n## Playable Contract\n\n- [ ] Define the first playable loop.\n")
    write_if_missing(root / "content/scenes/main.scene.json", json.dumps(scene, indent=2) + "\n")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
