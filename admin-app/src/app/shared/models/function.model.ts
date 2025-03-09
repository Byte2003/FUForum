export class Function {
    id: string;
    name: string;
    url: string;
    sortOrder: number;
    parentId: string;
    children?: Function[];

    constructor(id: string, name: string, url: string, sortOrder: number, parentId: string, children?: Function[]) {
        this.id = id;
        this.name = name;
        this.url = url;
        this.sortOrder = sortOrder;
        this.parentId = parentId;
        this.children = children || [];
    }
}