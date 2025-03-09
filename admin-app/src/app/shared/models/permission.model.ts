export class Permission{
    functionId: string;
    roleId: string;
    commandId: string;
    
    constructor(functionId: string, roleId: string, commandId: string){
        this.functionId = functionId;
        this.roleId = roleId;
        this.commandId = commandId;
    }
}