import { Chat } from "./Chats";
import type { Project } from "./Project";
import { User } from "./User";

export class Message {
    id: number;
    userId: number;
    chatId: number;
    text: string;
    date: Date = new Date();

    user: User;
    chat: Chat | Project;

    constructor(user: User, chat: Chat | Project) {
        this.id = 0;
        this.chatId = user.id;
        this.userId = chat.id;
        this.text = "";

        this.user = user;
        this.chat = chat;
    }
}