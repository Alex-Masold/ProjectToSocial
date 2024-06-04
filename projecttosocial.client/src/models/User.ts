import { Chat } from "./Chats";
import { Message } from "./Message";
import { Project } from "./Project";
import { Role } from "./Role";

export class User {
    id: number;
    roleId: number;
    firstName: string;
    lastName: string;
    family: string;
    email: string;
    password: string;

    role: Role;

    messages: Array<Message> = [];
    chats: Array<Chat> = [];
    projects: Array<Project> = [];
    
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