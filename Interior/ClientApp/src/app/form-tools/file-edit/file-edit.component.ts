import { BehaviorSubject } from "rxjs";
import { FileModel } from "src/app/models/File";
import { FileType } from "./../../models/Enums";
import {
  Component,
  Input,
  Output,
  EventEmitter,
  AfterContentChecked,
  OnInit
} from "@angular/core";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: "app-file-edit",
  templateUrl: "./file-edit.component.html",
  styleUrls: ["./file-edit.component.scss"]
})
export class FileEditComponent implements AfterContentChecked {
  @Output() changeFile = new EventEmitter();
  @Input() fileModel: FileModel = new FileModel();
  constructor(private sanitizer: DomSanitizer) {}

  fileToUpload: File = null;
  faSearch = faSearch;
  imagePath;
  imgURL: any;
  message: string;

  get fileName() {
    if (this.fileModel && this.fileModel.fileName)
      return this.fileModel.fileName;
    return "Choose File";
  }
  ngAfterContentChecked(): void {
    if (this.fileModel && this.fileModel.imageData) {
      let objectURL = "data:image/jpeg;base64," + this.fileModel.imageData;
      this.imgURL = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      this.fileModel.imageData = null;
    } else {
      this.fileModel = new FileModel();
    }
  }
  onFileChange(files: FileList) {
    if (files.length === 0) return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }
    this.imgURL = null;
    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]);
    reader.onload = _event => {
      this.imgURL = reader.result;
    };
    this.fileModel.fileName = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
    this.changeFile.emit({ file: this.fileToUpload, oldFile: this.fileModel });
    console.log(this.imgURL);
  }
  deleteFile() {
    this.imgURL = null;
    this.fileModel.fileName = "Choose File";
  }
}
