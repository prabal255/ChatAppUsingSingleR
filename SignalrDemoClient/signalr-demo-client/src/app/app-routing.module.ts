import { AuthComponent } from './auth/auth.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { RegistrationComponent } from './registration/registration.component';

//2Tutorial
const routes: Routes = [
  {
   
      path:'registration',
      component:RegistrationComponent
  },
  { 
    path: '', redirectTo: 'home', pathMatch: 'full' 
  },
  { 
    path: 'auth', component: AuthComponent 
  },
  //3Tutorial
  { 
    path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    canActivate: [AuthGuard]
  },

  { 
    path: '**', redirectTo: 'home', pathMatch: 'full'
   },

];

//3Tutorial
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
