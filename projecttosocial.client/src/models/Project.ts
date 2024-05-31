import { Message } from "./Message";
import { Task } from "./Task";
import { User } from "./User";

export class Project {
    id: number;
    name: string;

    users: Array<User> = [];
    tasks: Array<Task> = [];
    messages: Array<Message> = [];

    constructor() {
        this.id = 0;
        this.name = "";
    }
}