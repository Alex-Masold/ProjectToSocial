import type { Task } from "./Task";
import type { User } from "./User";

export interface Subtasks{
    id: number;
    idTask: number;
    idUser: number;
    content: string;
    completed: boolean;

    task: Task;
    user: User;
}