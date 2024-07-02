import { Chat } from "./Chat";
import { Message } from "./Message";
import { Project } from "./Project";
import { Role } from "./Role";
import type { Task } from "./Task";

export class User {
    id: number;
    idRole: number;
    firstName: string;
    lastName: string;
    family: string;
    email: string;
    password: string;
    avatar: string

    role: Role;

    chats: Array<Chat> = [];
    messages: Array<Message> = [];
    projects: Array<Project> = [];
    tasks: Array<Task> = [];
    subtasks: Array<Task> = [];    
    
    constructor(role: Role) {
        this.id = 0;
        this.idRole = role.id;
        this.firstName = "";
        this.lastName = "";
        this.family = "";
        this.email = "";
        this.password = "";
        this.avatar = "";

        this.role = role
    }

}