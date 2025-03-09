export class PermissionScreen {
    id: string;

    name: string;

    parentId: string;

    hasCreate: boolean;

    hasUpdate: boolean;

    hasDelete: boolean;

    hasView: boolean;

    hasApprove: boolean;
    
    constructor(id: string, name: string, parentId: string, hasCreate: boolean, hasUpdate: boolean, hasDelete: boolean, hasView: boolean, hasApprove: boolean) {
        this.id = id;
        this.name = name;
        this.parentId = parentId;
        this.hasCreate = hasCreate;
        this.hasUpdate = hasUpdate;
        this.hasDelete = hasDelete;
        this.hasView = hasView;
        this.hasApprove = hasApprove;
    }
}