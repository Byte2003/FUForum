import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { KnowledgeBasesComponent } from "./knowledge-bases/knowledge-bases.component";
import { CategoriesComponent } from "./categories/categories.component";
import { CommentsComponent } from "./comments/comments.component";
import { ReportsComponent } from "./reports/reports.component";
import { KnowledgeBasesDetailComponent } from "./knowledge-bases-detail/knowledge-bases-detail.component";

const routes: Routes = [
    {   path: '', 
        component: KnowledgeBasesComponent,
    },
    {   path: 'knowledge-bases', 
        component: KnowledgeBasesComponent,
    },
    {   path: 'knowledge-bases-detail', 
        component: KnowledgeBasesDetailComponent,
    },
    {   path: 'knowledge-bases-detail/:id', 
        component: KnowledgeBasesDetailComponent,
    },
    {   path: 'categories',
        component: CategoriesComponent, 
    },
    {   path: 'comments',
        component: CommentsComponent, 
    },
    {   path: 'reports',
        component: ReportsComponent, 
    }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports : [RouterModule]
})

export class ContentsRoutingModule { }