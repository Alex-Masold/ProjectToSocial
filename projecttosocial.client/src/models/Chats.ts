import { Message } from "./Message";
import { User } from "./User";

export class Chat {
    id: number;
    firstUserId: number;
    secondUserId: number; 
    
    firstUser: User;
    secondUser: User;
    
    messages: Array<Message> = [];
    constructor(firstUser: User, secondUser: User) {
        this.id = 0;
        this.firstUserId = firstUser.id;
        this.secondUserId = secondUser.id;

        this.firstUser = firstUser;
        this.secondUser = secondUser;
    }
}