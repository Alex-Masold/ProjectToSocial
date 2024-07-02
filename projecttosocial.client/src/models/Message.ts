import { Chat } from "./Chat";
import { Project } from "./Project";
import { User } from "./User";

export class Message 
{
    id: number;
    idUser: number;
    idChat: number;
    content: string;
    isEdit: boolean;
    date: Date = new Date();

    user: User;
    chat: Chat;
    project: Project;

    constructor(user: User, chat: Chat, project: Project) 
    {
        this.id = 0;
        this.idUser = chat.id;
        this.idChat = user.id;
        this.isEdit = false;
        this.content = "";

        this.user = user;
        this.chat = chat;
        this.project = project;
    }
}