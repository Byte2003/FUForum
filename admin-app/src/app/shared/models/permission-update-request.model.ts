import { Permission } from './permission.model';

export class PermissionUpdateRequest {
    permissions: Permission[];
    constructor(Permissions: Permission[]) {
        this.permissions = Permissions;

    }

}