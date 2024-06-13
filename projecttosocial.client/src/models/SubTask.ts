import type { Task } from "./Task";
import type { User } from "./User";

export class subtasks{
    id: number;
    idTask: number;
    idUser: number;
    content: string;
    completed: boolean;

    task: Task;
    user: User;

    constructor(task: Task, user: User)
    {
        this.id = 0;
        this.idTask = task.id;
        this.idUser = user.id;
        this.content = "";
        this.completed = false;

        this.task = task;
        this.user = user;
    }
}