import { Component, OnInit } from '@angular/core';
import { faUsers,faLanguage,faTrophy,faShoppingBasket,faBandAid,faCouch } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public samplePagesCollapsed = true;
  constructor() { }

  ngOnInit() {
  }

}
