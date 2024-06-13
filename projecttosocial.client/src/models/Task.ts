import { Project } from "./Project";
import type { subtasks } from "./SubTask";
import { User } from "./User";

export class Task {
    id: number;
    projectId: number;  
    userId: number;
    content: string;
    completed: boolean;

    project: Project;
    user: User;
    subtasks: Array<subtasks>;

    constructor(project: Project, user: User) {
        this.id = 0;
        this.projectId = project.id;
        this.userId = user.id;
        this.content = "";
        this.completed = false;

        this.project = project;
        this.user = user;
        this.subtasks = [];
    }
    
}