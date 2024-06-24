import { Message } from "./Message";
import { User } from "./User";

export class Chat {
    id: number;
    
    users: Array<User> = [];
    messages: Array<Message> = [];

    constructor() {
        this.id = 0;
    }
}