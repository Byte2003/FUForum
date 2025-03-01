import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Management'
          },
        children: [
            {
                path: '',
                redirectTo: 'dashboard',
                pathMatch: 'prefix'
            },
            {
                path: 'dashboard',
                loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
                data: {
                    title: 'Dashboard'
                }
            },
            {
                path: 'contents',
                loadChildren: () => import('./contents/contents.module').then(m => m.ContentsModule),
                data: {
                    title: 'Contents'
                }
            },
            {
                path: 'statistics',
                loadChildren: () => import('./statistics/statistics.module').then(m => m.StatisticsModule),
                data: {
                    title: 'Statistics'
                }   
            },
            {
                path: 'systems',
                loadChildren: () => import('./systems/systems.module').then(m => m.SystemsModule),
                data: {
                    title: 'Systems'
                }   
            },
        ]
    }
];