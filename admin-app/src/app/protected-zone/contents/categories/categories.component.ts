import { Component } from '@angular/core';
import { AuthService } from '../../../shared/services';
import { UserService } from '../../../shared/services';
import { CategoriesService } from '../../../shared/services/categories.service';
@Component({
  selector: 'app-categories',
  standalone: false,
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {

  constructor(private authService: AuthService, private categoriesService: CategoriesService, private userService: UserService) {  
    
  }

  
}
