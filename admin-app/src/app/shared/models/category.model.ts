export class Category {
    id: number;
    name: string;
    seoAlias: string;
    seoDescription: string;
    sortOrder: number;
    parentId?: number;
    numberOfTickets: number;

    constructor(id: number, name: string, seoAlias: string, seoDescription: string, sortOrder: number, numberOfTickets: number, parentId?: number,) {
        this.id = id;
        this.name = name;
        this.seoAlias = seoAlias;
        this.seoDescription = seoDescription;
        this.sortOrder = sortOrder;
        this.parentId = parentId;
        this.numberOfTickets = numberOfTickets;
    }
}