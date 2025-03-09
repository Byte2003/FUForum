export class Profile {
    sub: string;
    userName: string;
    role: string;
    email: string;
    permissions: string;

    constructor(sub: string, userName: string, role: string, email: string, permissions: string) {
        this.sub = sub;
        this.userName = userName;
        this.role = role;
        this.email = email;
        this.permissions = permissions;
    }
}