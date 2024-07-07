import type { Chat } from "./Chat";
import type { Project } from "./Project";
import type { User } from "./User";

export interface Message 
{
    id: number;
    idUser: number;
    idChat: number;
    content: string;
    isEdit: boolean;
    date: Date;

    user: User;
    chat: Chat;
    project: Project;
}