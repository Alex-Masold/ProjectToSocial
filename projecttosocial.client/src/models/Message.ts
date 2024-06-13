import { Chat } from "./Chats";
import { Project } from "./Project";
import { User } from "./User";

export class Message 
{
    id: number;
    userId: number;
    chatId: number;
    content: string;
    date: Date = new Date();

    user: User;
    chat: Chat;
    project: Project;

    constructor(user: User, chat: Chat, project: Project) 
    {
        this.id = 0;
        this.chatId = user.id;
        this.userId = chat.id;
        this.content = "";

        this.user = user;
        this.chat = chat;
        this.project = project;
    }
}