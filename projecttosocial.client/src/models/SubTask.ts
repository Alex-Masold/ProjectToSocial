import type { Task } from "./Task";
import type { User } from "./User";

export interface subtasks{
    id: number;
    idTask: number;
    idUser: number;
    content: string;
    completed: boolean;

    task: Task;
    user: User;
}