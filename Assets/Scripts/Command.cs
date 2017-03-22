using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command {
    protected virtual void Execute (Actor actor) {}
}

public class MoveRight : Command {
    protected override void Execute (Actor actor) {
        Movement.Move (actor, Vector3.right);
    }
}

public class MoveLeft: Command {
    protected override void Execute (Actor actor) {
        Movement.Move (actor, Vector3.left);
    }
}

public class MoveUp: Command {
    protected override void Execute (Actor actor) {
        Movement.Move (actor, Vector3.up);
    }
}

public class MoveDown: Command {
    protected override void Execute (Actor actor) {
        Movement.Move (actor, Vector3.down);
    }
}
