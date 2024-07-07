import type { Project } from "./Project";
import type { subtasks } from "./SubTask";
import type { User } from "./User";

export interface Task {
    id: number;
    idProject: number;  
    idUser: number;
    content: string;
    completed: boolean;

    project: Project;
    user: User;
    subtasks: Array<subtasks>;
}