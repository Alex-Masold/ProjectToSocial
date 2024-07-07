import type { Message } from "./Message";
import type { User } from "./User";

export interface Chat {
    id: number;
    
    users: Array<User>;
    messages: Array<Message>;
}