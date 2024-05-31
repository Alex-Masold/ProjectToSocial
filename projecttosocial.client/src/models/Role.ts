import { User } from "./User";

export class Role {
    id: number;
    name: string;

    users: Array<User> = [];

    constructor() {
        this.id = 0;
        this.name = "";
    }
}