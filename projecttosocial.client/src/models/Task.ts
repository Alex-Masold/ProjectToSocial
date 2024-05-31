import { Project } from "./Project";
import { User } from "./User";

export class Task {
    id: number;
    projectId: number;  
    userId: number;
    title: string;
    completed: boolean;

    project: Project;
    user: User;

    constructor(project: Project, user: User) {
        this.id = 0;
        this.projectId = project.id;
        this.userId = user.id;
        this.title = "";
        this.completed = false;

        this.project = project;
        this.user = user;
    }
    
}