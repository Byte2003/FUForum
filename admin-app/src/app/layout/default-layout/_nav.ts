import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Nội dung',
    url: '/contents',
    iconComponent: { name: 'cil-chart' },
    children: [
      {
        name: 'Danh mục',
        url: '/protected-zone/contents/categories',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Bài viết',
        url: '/protected-zone/contents/knowledge-bases',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Báo xấu',
        url: '/protected-zone/contents/reports',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Bình luận',
        url: '/protected-zone/contents/comments',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Thống kê',
    url: '/statistics',
    iconComponent: { name: 'cil-chart' },
    children: [
      {
        name: 'Đăng ký từng tháng',
        url: '/protected-zone/statistics/monthly-new-members',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Bài đăng hàng tháng',
        url: '/protected-zone/statistics/monthly-new-kbs',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Comment theo tháng',
        url: '/protected-zone/statistics/monthly-new-comments',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Hệ thống',
    url: '/protected-zone/systems',
    iconComponent: { name: 'cil-settings' },
    children: [
      {
        name: 'Chức năng',
        url: '/protected-zone/systems/functions',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Quyền hạn',
        url: '/protected-zone/systems/permissions',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Nhóm quyền',
        url: '/protected-zone/systems/roles',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Người dùng',
        url: '/protected-zone/systems/users',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Thống kê',
    url: '/protected-zone/dashboard',
    iconComponent: { name: 'cil-speedometer' },
  }
];