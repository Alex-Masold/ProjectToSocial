import type { Message } from "./Message";
import type { Task } from "./Task";
import type { User } from "./User";

export interface Project {
    id: number;
    name: string;

    users: Array<User>;
    tasks: Array<Task>;
    messages: Array<Message> ;
}