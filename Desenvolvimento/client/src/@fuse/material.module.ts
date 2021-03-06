import { NgModule } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import {
    MatInputModule,
    MatTableModule,
    MatTooltipModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatDividerModule,
    MatToolbarModule,
    MatProgressBarModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSliderModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatGridListModule,
    MatChipsModule,
    MatDialogModule,
    MatSortModule,
    MatAutocompleteModule,
    MatRippleModule,
    MatOptionModule,
    MatTreeModule,
    MatTabsModule
} from '@angular/material';


const classesToInclude = [
    MatInputModule,
    MatTableModule,
    MatTooltipModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatDividerModule,
    MatToolbarModule,
    MatProgressBarModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSliderModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatGridListModule,
    MatChipsModule,
    MatDialogModule,
    MatSortModule,
    MatAutocompleteModule,
    MatRippleModule,
    MatOptionModule,
    MatTreeModule,
    MatTabsModule
]

@NgModule({
    imports: classesToInclude,
    exports: classesToInclude,
    providers: [
        MediaMatcher,
    ]
})
export class MaterialModule { }
