import type { Chat } from "./Chat";
import type { Message } from "./Message";
import type { Project } from "./Project";
import type { Role } from "./Role";
import type { Subtasks } from "./SubTask";
import type { Task } from "./Task";

export interface User {
    id: number;
    idRole: number;
    firstName: string;
    lastName: string;
    family: string;
    email: string;
    password: string;
    avatar: string

    role: Role;

    chats: Array<Chat>;
    messages: Array<Message>;
    projects: Array<Project>;
    tasks: Array<Task>;
    subtasks: Array<Subtasks>;    
}