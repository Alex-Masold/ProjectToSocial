import { Project } from "./Project";
import type { subtasks } from "./SubTask";
import { User } from "./User";

export class Task {
    id: number;
    idProject: number;  
    idUser: number;
    content: string;
    completed: boolean;

    project: Project;
    user: User;
    subtasks: Array<subtasks>;

    constructor(project: Project, user: User) {
        this.id = 0;
        this.idProject = project.id;
        this.idUser = user.id;
        this.content = "";
        this.completed = false;

        this.project = project;
        this.user = user;
        this.subtasks = [];
    }
    
}