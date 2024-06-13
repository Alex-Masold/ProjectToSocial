import { Chat } from "./Chats";
import { Message } from "./Message";
import { Project } from "./Project";
import { Role } from "./Role";
import type { Task } from "./Task";

export class User {
    id: number;
    roleId: number;
    firstName: string;
    lastName: string;
    family: string;
    email: string;
    password: string;

    role: Role;

    сhatFirstUser: Array<Chat> = [];
    chatSecondUser: Array<Chat> = [];
    messages: Array<Message> = [];
    projects: Array<Project> = [];
    tasks: Array<Task> = [];
    subtasks: Array<Task> = [];    
    
    constructor(role: Role) {
        this.id = 0;
        this.roleId = role.id;
        this.firstName = "";
        this.lastName = "";
        this.family = "";
        this.email = "";
        this.password = "";

        this.role = role
    }

}