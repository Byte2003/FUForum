import { Component, OnInit, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FunctionsService, CommandsService } from '../../../../shared/services';
import { CommandAssign } from '../../../../shared/models';

@Component({
  selector: 'app-commands-assign',
  standalone: false,
  templateUrl: './commands-assign.component.html',
  styleUrls: ['./commands-assign.component.scss']
})
export class CommandsAssignComponent implements OnInit {
  public blockedPanel = false;
  public items: any[] = [];
  public selectedItems: any[] = [];
  public dialogTitle!: string;
  public functionId!: string;
  public existingCommands: any[] = [];
  public addToAllFunctions = false;
  public chosenEvent: EventEmitter<any> = new EventEmitter();

  constructor(
    public bsModalRef: BsModalRef,
    private functionsService: FunctionsService,
    private commandsService: CommandsService) {
  }

  ngOnInit() {
    this.loadAllCommands();
  }

  loadAllCommands() {
    this.blockedPanel = true;
    this.commandsService.getAll()
      .subscribe((response: any) => {
        const existingCommands = this.existingCommands;
        const notExistingCommands: any[] = response.filter((item: any) => !existingCommands.includes(item.id));

        this.items = notExistingCommands;
        if (this.selectedItems.length === 0 && this.items.length > 0) {
          this.selectedItems.push(this.items[0]);
        }
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      });
  }

  chooseCommands() {
    this.blockedPanel = true;
    const selectedItemIds = this.selectedItems.map(element => element.id);
    const entity = new CommandAssign();
    entity.addToAllFunctions = this.addToAllFunctions;
    entity.commandIds = selectedItemIds;

    this.functionsService.addCommandsToFunction(this.functionId, entity).subscribe(() => {
      this.chosenEvent.emit(this.selectedItems);
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    });
  }
}